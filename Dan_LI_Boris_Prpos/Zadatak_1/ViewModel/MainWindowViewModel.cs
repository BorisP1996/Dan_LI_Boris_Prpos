using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        Entity context = new Entity();

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
           
        }
     
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        private void LoginExecute()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
         
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand patient;
        public ICommand Patient
        {
            get
            {
                if (patient==null)
                {
                    patient = new RelayCommand(param => PatientExecute(), param => CanPatientExecute());
                }
                return patient;
            }
        }
        private void PatientExecute()
        {
            try
            {
                CreatePatient createPatient = new CreatePatient();
                createPatient.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanPatientExecute()
        {
            return true;
        }
        private ICommand doctor;
        public ICommand Doctor
        {
            get
            {
                if (doctor == null)
                {
                    doctor = new RelayCommand(param => DoctorExecute(), param => CanDoctorExecute());
                }
                return doctor;
            }
        }
        private void DoctorExecute()
        {
            try
            {
                CreateDoctor createDoctor = new CreateDoctor();
                createDoctor.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDoctorExecute()
        {
            return true;
        }

    }
}
