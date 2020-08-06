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
    class AbsenceViewModel : ViewModelBase
    {
        AbsenceView absenceView;
        Entity context = new Entity();

        public AbsenceViewModel(AbsenceView abOpen,string username)
        {
            absenceView = abOpen;
            InputUser = username;
            Absence = new tblAbsence();
        }
        private string inputUser;

        public string InputUser
        {
            get { return inputUser; }
            set
            {
                inputUser = value;
                OnPropertyChanged("InputUser");
            }
        }

        private bool urgent;

        public bool Urgent
        {
            get { return urgent; }
            set { urgent = value;
                OnPropertyChanged("Urgent");
            }
        }
        private bool update;
        public bool Update
        {
            get
            {
                return update;

            }
            set
            {
                update = value;
                OnPropertyChanged("Update");
            }
        }

        private tblAbsence absence;

        public tblAbsence Absence
        {
            get { return absence; }
            set { absence = value;
                OnPropertyChanged("Absence");
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
                tblPatient viaPatient = (from r in context.tblPatients where r.Username == InputUser select r).FirstOrDefault();

                tblAbsence newAbsence = new tblAbsence();
                newAbsence.RequestDate = DateTime.Now;
                newAbsence.Company = Absence.Company;
                newAbsence.Reason = Absence.Reason;
                newAbsence.PatientID = viaPatient.PatientID;
                newAbsence.Responsed = false;
                newAbsence.Urgent = Urgent;
                context.tblAbsences.Add(newAbsence);
                context.SaveChanges();
                MessageBox.Show("Request absence is sent");
                Absence = new tblAbsence();
                Update = true;
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Absence.Reason)||String.IsNullOrEmpty(Absence.Company))
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
            absenceView.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }




    }
}
