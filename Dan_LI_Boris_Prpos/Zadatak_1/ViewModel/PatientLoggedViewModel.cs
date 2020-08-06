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
    class PatientLoggedViewModel:ViewModelBase
    {
        Entity context = new Entity();
        PatientLogged patLogged;

        public PatientLoggedViewModel(PatientLogged patLogedOpen,string usernameInput)
        {
            patLogged = patLogedOpen;
            InputUser = usernameInput;
            AbsenceList = getAbsences();
        }

        private List<tblAbsence> absenceList;

        public List<tblAbsence> AbsenceList
        {
            get { return absenceList; }
            set { absenceList = value;
                OnPropertyChanged("AbsenceList");
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


        private bool update;

        public bool Update
        {
            get { return update; }
            set { update = value; }
        }
        private string inputUser;

        public string InputUser
        {
            get { return inputUser; }
            set { inputUser = value;
                OnPropertyChanged("InputUser");
            }
        }
        
        public bool CheckForDoctor(string username)
        {
            try
            {
                tblPatient viaPatient = (from r in context.tblPatients where r.Username == username select r).FirstOrDefault();
                if (viaPatient.DoctorID==null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private ICommand select;
        public ICommand Select
        {
            get
            {
                if (select==null)
                {
                    select = new RelayCommand(param => SelectExecute(), param => CanSelectExecute());
                }
                return select;
            }
        }

        private void SelectExecute()
        {
            try
            {
                SelectDoctorView sdv = new SelectDoctorView(InputUser);
                sdv.ShowDialog();
                if ((sdv.DataContext as SelectDoctorViewModel).Update == true)
                {
                    Update = true;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSelectExecute()
        {
            if (CheckForDoctor(InputUser) == true || Update == true)
            {
                return true;
            }                  
            else
            {
                return false;
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
           
            patLogged.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        private ICommand request;
        public ICommand Request
        {
            get
            {
                if (request == null)
                {
                    request = new RelayCommand(param => RequestExecute(), param => CanRequestExecute());
                }
                return request;
            }
        }

        private void RequestExecute()
        {
            try
            {
                AbsenceView absence = new AbsenceView(InputUser);
                //absence.ShowDialog();
                tblPatient viaPatient = (from r in context.tblPatients where r.Username == InputUser select r).FirstOrDefault();
                if (viaPatient.DoctorID == null)
                {
                    MessageBox.Show("In order to request absence, you need to have doctor selected.");
                    absence.ShowDialog();
                }
                else
                {
                    absence.ShowDialog();
               }
                if ((absence.DataContext as AbsenceViewModel).Update==true)
                {
                    AbsenceList = getAbsences();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanRequestExecute()
        {
              return true;
            
        }
        private List<tblAbsence> getAbsences()
        {
            try
            {
                List<tblAbsence> listAll = context.tblAbsences.ToList();

                List<tblAbsence> listMine = new List<tblAbsence>();
                tblPatient viaPatient = (from r in context.tblPatients where r.Username == InputUser select r).FirstOrDefault();

                foreach (tblAbsence item in listAll)
                {
                    if (item.PatientID==viaPatient.PatientID)
                    {
                        listMine.Add(item);
                    }
                }
                return listMine;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
            }
        }

    }
}
