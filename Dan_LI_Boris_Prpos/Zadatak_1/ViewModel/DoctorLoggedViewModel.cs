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
    class DoctorLoggedViewModel:ViewModelBase
    {
        DoctorLogged docLoged;
        Entity context = new Entity();

        public DoctorLoggedViewModel(DoctorLogged docLogedOpen,string username)
        {
            docLoged = docLogedOpen;
            InputUser = username;
            ViewList = GetViews();
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
        private List<vwPatient> viewList;
        public List<vwPatient> ViewList
        {
            get
            {
                return viewList;
            }
            set
            {
                viewList = value;
                OnPropertyChanged("ViewList");
            }
        }
        private vwPatient view;
        public vwPatient View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                OnPropertyChanged("View");
            }
        }
        private List<vwPatient> GetViews()
        {
            try
            {
                tblDoctor viaDoctor = (from r in context.tblDoctors where r.Username == InputUser select r).FirstOrDefault();

                List<vwPatient> allViews = context.vwPatients.ToList();
                List<vwPatient> myPatients = new List<vwPatient>();

                foreach (vwPatient item in allViews)
                {
                    if (item.DoctorID==viaDoctor.DoctorID)
                    {
                        myPatients.Add(item);
                    }
                }
                return myPatients;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return null;
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
            docLoged.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        }
}
