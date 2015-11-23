using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duv.Domain.Triggers.Models;
using Duv.Domain.Triggers.Repositories;

namespace Duv.Domain.Triggers.Services
{
	public class MonitoringFileService : IMonitoringFileService
	{
		private readonly IRepositoryLocator locator;

		public MonitoringFileService(IRepositoryLocator locator)
		{
			this.locator = locator;
		}

		public IList<MonitoringFile> FindAllFiles()
		{
			var result = locator.FindAll<MonitoringFile>();
			return result.Select(e => EntityToModel(e)).ToList();
		}

		public MonitoringFile GetFileByPath(string fileName)
		{
			var result = locator.FindAll<MonitoringFile>().FirstOrDefault(e => e.Name == fileName);
			return (result == null) ? null : EntityToModel(result);
		}

		public MonitoringFile CreateFile(MonitoringFile model)
		{
			var entity = new MonitoringFile();
			entity.Name = model.Name;
			entity.Modified = model.Modified;
			entity.Size = model.Size;
			locator.Insert(entity);

			AddDocuments(entity, model.Documents);
			entity.IsLoaded = true;
			locator.Update(entity);

			entity = locator.GetById<MonitoringFile>(entity.Id); // to read Loaded (db-side datetime)
			return EntityToModel(entity);
		}

		public MonitoringFile UpdateFile(MonitoringFile model)
		{
			var entity = locator.GetById<MonitoringFile>(model.Id);
			entity.Modified = model.Modified;
			entity.Size = model.Size;
			locator.Update(entity);

			return EntityToModel(entity);
		}

		public MonitoringFile UploadFile(MonitoringFile model)
		{
			var entity = locator.FindAll<MonitoringFile>().FirstOrDefault(e => e.Name == model.Name);
			if (entity == null)
			{
				entity = new MonitoringFile();
				entity.Name = model.Name;
				entity.Modified = model.Modified;
				entity.Size = model.Size;
				locator.Insert(entity);

				AddDocuments(entity, model.Documents);
			}
			else
			{
				entity.Modified = model.Modified;
				entity.Size = model.Size;
				locator.Update<MonitoringFile>(entity);

				MergeFileDocuments(entity, model.Documents);
			}
			entity.IsLoaded = true;
			locator.Update(entity);

			entity = locator.GetById<MonitoringFile>(entity.Id); // to read Loaded (db-side datetime)
			return EntityToModel(entity);
		}

		public void DeleteFile(long fileId)
		{
			var entity = locator.GetById<MonitoringFile>(fileId);
			locator.Delete(entity);
		}

		public IList<MonitoringDocument> FindDocuments(long fileId)
		{
			var result = locator.FindAll<MonitoringDocument>().Where(e => e.FileId == fileId);
			return result.Select(e => EntityToModel(e)).ToList();
		}

		protected void MergeFileDocuments(MonitoringFile file, IEnumerable<MonitoringDocument> fileDocuments)
		{
			AddDocuments(file, fileDocuments);
			foreach (var fileDocument in file.Documents)
			{
				if (!fileDocuments.Any(e => e.DocumentId == fileDocument.DocumentId))
				{
					locator.Delete(fileDocument);
					file.Documents.Remove(fileDocument);
				}
			}
		}

		protected void AddDocuments(MonitoringFile file, IEnumerable<MonitoringDocument> fileDocuments)
		{
			file.Documents = locator.FindAll<MonitoringDocument>().Where(e => e.FileId == file.Id).ToList();
			foreach (var model in fileDocuments)
			{
				model.FileId = file.Id;
				var document = GetCustomerDocumentByNumber(model);
				if (document == null)
				{
					document = CreateCustomerDocument(model);
				}
				model.CustomerId = document.CustomerId;
				model.PersonId = document.PersonId;
				model.DocumentId = document.Id;

				if (!file.Documents.Any(e => e.DocumentId == model.DocumentId))
				{
					var fileDocument = CreateMonitoringDocument(file, model);
					file.Documents.Add(fileDocument);
				}
			}
		}

		protected void DeleteDocuments(MonitoringFile file, IEnumerable<MonitoringDocument> fileDocuments)
		{
			file.Documents = locator.FindAll<MonitoringDocument>().Where(e => e.FileId == file.Id).ToList();
			foreach (var model in fileDocuments)
			{
				var fileDocument = file.Documents.FirstOrDefault(e => e.DocumentId == model.DocumentId);
				if (fileDocument != null)
				{
					locator.Delete(fileDocument);
					file.Documents.Remove(fileDocument);
				}
			}
		}

		private CustomerDocument GetCustomerDocumentByNumber(MonitoringDocument model)
		{
			var documentType = locator.FindAll<PersonDocumentType>()
				.FirstOrDefault(e => e.Code == model.TypeCode);
			model.TypeId = (documentType != null) ? documentType.Id : 0;

			var document = locator.FindAll<CustomerDocument>()
				.FirstOrDefault(e => e.TypeId == model.TypeId && e.Series == model.Series && e.Number == model.Number);
			return document;
		}

		private CustomerDocument CreateCustomerDocument(MonitoringDocument model)
		{
			CustomerNumber customer = GetCustomerByNumber(model);
			if (customer == null)
			{
				customer = CreateCustomer(model);
			}
			model.CustomerId = customer.CustomerId;
			model.PersonId = customer.PersonId;

			CustomerDocument document = new CustomerDocument();
			document.CustomerId = model.CustomerId;
			document.PersonId = model.PersonId;
			document.TypeId = model.TypeId;
			document.Series = model.Series;
			document.Number = model.Number;
			document.IssueDate = model.IssueDate;
			document.IssueLocation = model.IssueLocation;
			document.IssueAuthority = model.IssueAuthority;

			locator.Insert(document);
			return document;
		}

		private CustomerNumber GetCustomerByNumber(MonitoringDocument model)
		{
			return locator.FindAll<CustomerNumber>()
				.FirstOrDefault(e => e.Number == model.CustomerNumber && e.LastName == model.LastName && e.MiddleName == model.MiddleName && e.FirstName == e.FirstName);
		}

		private CustomerNumber CreateCustomer(MonitoringDocument model)
		{
			CustomerNumber customer = new CustomerNumber();
			customer.Number = model.CustomerNumber;
			customer.LastName = model.LastName;
			customer.MiddleName = model.MiddleName;
			customer.FirstName = model.FirstName;
			customer.BirthDate = model.BirthDate;
			customer.BirthPlace = model.BirthPlace;

			locator.Insert(customer);
			customer = locator.GetById<CustomerNumber>(customer.Id); // to read PersonId
			return customer;
		}

		private MonitoringDocument CreateMonitoringDocument(MonitoringFile file, MonitoringDocument model)
		{
			var fileDocument = new MonitoringDocument();
			fileDocument.FileId = file.Id;
			//fileDocument.CustomerId = model.CustomerId;
			//fileDocument.PersonId = model.PersonId;
			fileDocument.DocumentId = model.DocumentId;
			fileDocument.CustomerNumber = model.CustomerNumber;
			fileDocument.ApplicationId = model.ApplicationId;

			locator.Insert(fileDocument);
			return fileDocument;
		}

		private MonitoringFile EntityToModel(MonitoringFile entity)
		{
			var model = new MonitoringFile();
			model.Id = entity.Id;
			model.Name = entity.Name;
			model.Modified = entity.Modified;
			model.Size = entity.Size;
			model.Loaded = entity.Loaded;

			return model;
		}

		private MonitoringDocument EntityToModel(MonitoringDocument entity)
		{
			var model = new MonitoringDocument();
			return EntityToModel(entity, model);
		}

		private MonitoringDocument EntityToModel(MonitoringDocument entity, MonitoringDocument model)
		{
			model.Id = entity.Id;
			model.FileId = entity.FileId;
			// customer info
			model.CustomerId = entity.CustomerId;
			model.CustomerNumber = entity.CustomerNumber;
			model.ApplicationId = entity.ApplicationId;
			// person info
			model.PersonId = entity.PersonId;
			model.LastName = entity.LastName;
			model.MiddleName = entity.MiddleName;
			model.FirstName = entity.FirstName;
			model.BirthDate = entity.BirthDate;
			model.BirthPlace = entity.BirthPlace;
			// id document info
			model.DocumentId = entity.DocumentId;
			model.TypeId = entity.TypeId;
			model.TypeCode = entity.TypeCode;
			model.Series = entity.Series;
			model.Number = entity.Number;
			model.IssueDate = entity.IssueDate;
			model.IssueLocation = entity.IssueLocation;
			model.IssueAuthority = entity.IssueAuthority;

			return model;
		}
	}
}
