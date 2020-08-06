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
    class CreatePatientViewModel:ViewModelBase
    {
        CreatePatient createPat;
        Entity context = new Entity();

        public CreatePatientViewModel(CreatePatient createPatOpen)
        {
            createPat = createPatOpen;
            Patient = new tblPatient();
        }

        private tblPatient patient;

        public tblPatient Patient
        {
            get { return patient; }
            set { patient = value;
                OnPropertyChanged("Patient");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save==null)
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
               tblPatient tblPatient = new tblPatient();
                tblPatient.Firstname = Patient.Firstname;
                tblPatient.Lastname = Patient.Lastname;
                tblPatient.JMBG = Patient.JMBG;
                tblPatient.HealthNumber = Patient.HealthNumber;
                tblPatient.Username = Patient.Username;
                tblPatient.Pasword = Patient.Pasword;
                context.tblPatients.Add(tblPatient);
                context.SaveChanges();
                MessageBox.Show("Patient is saved");
                Patient = new tblPatient();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Patient.Firstname) || String.IsNullOrEmpty(Patient.Lastname) || String.IsNullOrEmpty(Patient.JMBG) || String.IsNullOrEmpty(Patient.Username) || String.IsNullOrEmpty(Patient.Pasword) || String.IsNullOrWhiteSpace(Patient.HealthNumber))
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
            createPat.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
    }
}
