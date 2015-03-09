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

            DeviceTestCommand = new RelayCommand(DoTestDevice, CanTestDevice);
        }

        private bool CanTestDevice(object obj)
        {
            return true;
        }

        private void DoTestDevice(object obj)
        {
            Device.ExecuteCommand("TEST");
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

        public RelayCommand DeviceTestCommand { get; set; }
    }
}
