using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Model;
using Zadatak_1.Tools;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class CreateDoctorViewModel:ViewModelBase
    {
        CreateDoctor createDoc;
        Entity context = new Entity();
        Methods method = new Methods();

        public CreateDoctorViewModel(CreateDoctor createDocOpen)
        {
            createDoc = createDocOpen;
            Doctor = new tblDoctor();
        }

        private tblDoctor doctor;

        public tblDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }
        private void SaveExecute()
        {
            try
            {
                tblDoctor newDoctor = new tblDoctor();
                newDoctor.Firstname = Doctor.Firstname;
                newDoctor.Lastname = Doctor.Lastname;
                newDoctor.JMBG = Doctor.JMBG;
                newDoctor.AccountNumber = Doctor.AccountNumber;
                newDoctor.Username = Doctor.Username;
                newDoctor.Pasword = Doctor.Pasword;
                if (method.ValidateCredentials(newDoctor.Username,newDoctor.Pasword,newDoctor.AccountNumber,newDoctor.JMBG)==true && method.ValiationJMBG(newDoctor.JMBG))
                {
                    context.tblDoctors.Add(newDoctor);
                    context.SaveChanges();
                    MessageBox.Show("Doctor is saved");
                    Doctor = new tblDoctor();
                }
                else
                {
                    MessageBox.Show("Error input\nCheck following:\nUsername must be unique\nPaswword must be unique\n ");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Doctor.Firstname) || String.IsNullOrEmpty(Doctor.Lastname) || String.IsNullOrEmpty(Doctor.JMBG) || String.IsNullOrEmpty(Doctor.Username) || String.IsNullOrEmpty(Doctor.Pasword) || String.IsNullOrWhiteSpace(Doctor.AccountNumber))
            {
                return false;
            }
            else
            {
                return true;
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
            createDoc.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
    }
}
