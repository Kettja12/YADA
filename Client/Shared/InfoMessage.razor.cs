using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shared
{
    public partial class InfoMessage
    {
        private bool IsHidden { get; set; } = true;
        private string Value = "OK";

        private string alarmType = "alert-danger";

        /// <summary>
        /// InfoMessage classes
        /// </summary>
        /// <param name="type">
        ///     alert-primary,alert-secondary,alert—check,alert-success,alert—check,
        ///alert-danger,alert—check,alert-warning,alert—check,alert-info,alert—check,alert-light
        ///alert-dark</param>

        public async void Show(string type, string value)
        {
            IsHidden = false;
            alarmType = type;
            Value = value;
            StateHasChanged();
            await Task.Delay(5000);
            Hide();

        }
        public void Hide()
        {
            IsHidden = true;
            Value = "";
            StateHasChanged();

        }

    }
}
