﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilmPage : ContentPage
	{
		public AddFilmPage ()
		{
			InitializeComponent ();
            Movie AddingMovie = new Movie();
		}

        public void OnCheckButtonPressed()
        {

        }
	}
}