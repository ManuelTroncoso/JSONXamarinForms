using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FirstApp
{
    //Página de contenido// Form blank ContentPage
	public class MainPage : ContentPage
	{
        Entry mPhoneNumberText;
        Button mTranslateButton;
        Button mCallButton;

        string mTranslateNumber;

        public MainPage()
        {
            this.Padding = new Thickness(20);
            StackLayout panel = new StackLayout();
            panel.Spacing = 15;

            panel.Children.Add(new Label {
                Text = "Introduce tu número de telefono",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            });

            panel.Children.Add(mPhoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN"
            });

            panel.Children.Add(mTranslateButton = new Button() {
                Text = "Translate"
            });

            panel.Children.Add(mCallButton = new Button()
            {
                Text = "Call", 
                IsEnabled = false 
            });

            mTranslateButton.Clicked += TranslateButton_Clicked;
            mCallButton.Clicked += CallButton_Clicked;
            this.Content = panel;
        }

        private async void CallButton_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert("Seguro?", "Estas seguro de que quieres llamar a " + mTranslateNumber + "?", "Sí", "No"))
            {
                IDialer dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    await dialer.DialAsync(mTranslateNumber);

            } 
        }

        private void TranslateButton_Clicked(object sender, EventArgs e)
        {
            mTranslateNumber = Core.PhonewordTranslator.ToNumber(mPhoneNumberText.Text);
            if (!string.IsNullOrEmpty(mTranslateNumber))
            {
                mCallButton.IsEnabled = true;
                mCallButton.Text = "Call" + mTranslateNumber;
            }
            else
            {
                mCallButton.IsEnabled = false;
                mCallButton.Text = "Call";
            }

        }
    }
}