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
    class SelectDoctorViewModel:ViewModelBase
    {
        Entity context = new Entity();
        SelectDoctorView selDoc;
        public SelectDoctorViewModel(SelectDoctorView selectDoc,string username)
        {
            selDoc = selectDoc;
            InputUser = username;
            Doctor = new tblDoctor();
            DoctorList = GetDoctors();
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
            set
            {
                inputUser = value;
                OnPropertyChanged("InputUser");
            }
        }
        private tblDoctor doctor;

        public tblDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        private List<tblDoctor> doctorList ;

        public List<tblDoctor> DoctorList 
        {
            get { return doctorList ; }
            set {  doctorList= value;
                OnPropertyChanged("DoctorList");
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

                
                if (DoctorList.Count == 0)
                {
                    MessageBox.Show("There are no available doctors");
                }
                if (viaPatient.DoctorID != null)
                {
                    MessageBox.Show("You can select doctor only once.");
                }
                else
                {
                    viaPatient.DoctorID = Doctor.DoctorID;
                    context.SaveChanges();
                    MessageBox.Show("Doctor is assigned to you.");
                    Update = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Doctor.Lastname))
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
            selDoc.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        private List<tblDoctor> GetDoctors()
        {
            try
            {
                List<tblDoctor> doctorList = new List<tblDoctor>();
                doctorList = context.tblDoctors.ToList();
                return doctorList;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
