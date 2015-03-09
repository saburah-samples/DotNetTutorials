using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HelloWpf
{
    public class MainDevice : INotifyDeviceStateChanged
    {
        public void ExecuteCommand(string command, params string[] args)
        {
            Command = command;
            NotifyChanged();
        }

        protected void NotifyChanged()
        {
            NotifyStateChanged(State, Command);
        }

        private void NotifyStateChanged(string state, string command)
        {
            if (OnStateChanged != null)
                OnStateChanged(this, new DeviceStateChangedEventArgs(state, command));
        }

        public string Command { get; private set; }
        public string State { get; private set; }

        public event DeviceStateChangedEventHandler OnStateChanged;
    }

    public interface INotifyDeviceStateChanged
    {
        event DeviceStateChangedEventHandler OnStateChanged;
    }

    public class DeviceStateChangedEventArgs : EventArgs
    {
        public DeviceStateChangedEventArgs(string state, string command)
        {
            State = state;
            Command = command;
        }

        public virtual string State { get; private set; }
        public virtual string Command { get; private set; }
    }

    public delegate void DeviceStateChangedEventHandler(object sender, DeviceStateChangedEventArgs e);

}
