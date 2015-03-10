using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HelloWpf
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            CommandLog = new ObservableCollection<string>();
            Device = new MainDevice();
            Device.OnStateChanged += Device_OnStateChanged;

            DeviceStartTestCommand = new RelayCommand(DoStartTestDevice, CanStartTestDevice);
            DeviceCompleteTestCommand = new RelayCommand(DoCompleteTestDevice, CanCompleteTestDevice);
        }

        private bool CanCompleteTestDevice(object obj)
        {
            return true;
        }

        private void DoCompleteTestDevice(object obj)
        {
            Device.ExecuteCommand("Complete test");
        }

        private bool CanStartTestDevice(object obj)
        {
            return true;
        }

        private void DoStartTestDevice(object obj)
        {
            Device.ExecuteCommand("Start test");
        }

        void Device_OnStateChanged(object sender, DeviceStateChangedEventArgs e)
        {
            CommandLog.Add(string.Format("{0:H:mm:ss.fff}: {1}-{2}", DateTime.Now, e.Command, e.State));
        }
        
        /// <summary>
        /// Журнал комманд
        /// </summary>
        public ICollection<string> CommandLog { get; private set; }

        /// <summary>
        /// Устройство
        /// </summary>
        public MainDevice Device { get; private set; }

        public RelayCommand DeviceStartTestCommand { get; set; }

        public RelayCommand DeviceCompleteTestCommand { get; set; }
    }
}
