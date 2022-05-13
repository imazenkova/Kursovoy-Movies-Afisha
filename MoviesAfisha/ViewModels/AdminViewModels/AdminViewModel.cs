using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.View;
using MoviesAfisha.View.AdminWindows;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoviesAfisha.ViewModels.AdminViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public TabItem SelectedTabItem { get; set; }
        public User SelectedUser { get; set; }
        public Cinemas SelectedCinema { get; set; }

        private List<User> allUsers;
        public List<User> AllUsers
        {
            get { return allUsers; }
            set
            {
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }
        
        private List<AdminCinema> allAdminCinemas;
        public List<AdminCinema> AllAdminCinemas
        {
            get { return allAdminCinemas; }
            set
            {
                allAdminCinemas = value;
                NotifyPropertyChanged("AllAdminCinemas");
            }
        }
       
        private List<Cinemas> allCinemas;
        public List<Cinemas> AllCinemas
        {
            get { return allCinemas; }
            set
            {
                allCinemas = value;
                NotifyPropertyChanged("AllAdminCinemas");
            }
        }
       
        public AdminViewModel()
        {
            allUsers = new UnitOfWork().User.GetAll();
            allAdminCinemas = new UnitOfWork().AdminCinema.GetAll();
            allCinemas = new UnitOfWork().Cinemas.GetAll();
        }

        private RelayCommand deleteItem=null;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    UnitOfWork unitOfWork = new UnitOfWork();
                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        unitOfWork.OrderTicket.Delete(SelectedUser.Id);
                        unitOfWork.User.Delete(SelectedUser.Id);
                        resultStr = "Пользователь удален";
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "CinemaTab" && SelectedCinema != null)
                    {
                        UpdateAllDataView();
                    }
                    MessageWindowCommand.ShowMessageToUser(resultStr);
                });
            }
        }

        #region COMMANDS Open windows
        private RelayCommand openAddNewUserWindow = null;
        private RelayCommand openAddNewAdminCinemaWindow = null;
        private RelayCommand openEditUserWindow = null;
        public RelayCommand OpenAddNewUserWindow
        {
            get
            {
                return openAddNewUserWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenAddNewUserWindowCommand();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        public RelayCommand OpenAddNewAdminCinemaWindow
        {
            get
            {
                return openAddNewAdminCinemaWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenAddNewAdminCinemaWindowCommand();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        public RelayCommand OpenEditUserWindow
        {
            get
            {
                return openEditUserWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        EditUserPasswordWindowCommand();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }

        #endregion

        #region COMMANDS Update

        private RelayCommand refreshData = null;
        public RelayCommand RefreshData
        {
            get
            {
                return refreshData ?? new RelayCommand(obj =>
                {
                    UpdateAllDataView();
                });
            }
        }
        #endregion

        #region METHODS Update
        private void UpdateAllDataView()
        {
            UpdateAllUsersView();
            UpdateAllCinemasView();
        }
        private void UpdateAllUsersView()
        {
            AllUsers = new UnitOfWork().User.GetAll();
            AdminWindow.AllUsers.ItemsSource = null;
            AdminWindow.AllUsers.Items.Clear();
            AdminWindow.AllUsers.ItemsSource = AllUsers;
            AdminWindow.AllUsers.Items.Refresh();
        }
        private void UpdateAllCinemasView()
        {
            AllCinemas = new UnitOfWork().Cinemas.GetAll();
            AdminWindow.AllCinemas.ItemsSource = null;
            AdminWindow.AllCinemas.Items.Clear();
            AdminWindow.AllCinemas.ItemsSource = AllCinemas;
            AdminWindow.AllCinemas.Items.Refresh();
        }

        #endregion

        #region METHODS Open windows
        private void OpenAddNewUserWindowCommand()
        {
            AddUserWindow newUserWindow = new AddUserWindow();
            SetCenterPositionAndOpen(newUserWindow);
        }
        private void OpenAddNewAdminCinemaWindowCommand()
        {
            AddAdminCinemaWindow newAdminCinemaWindow = new AddAdminCinemaWindow();
            SetCenterPositionAndOpen(newAdminCinemaWindow);
        }
        private void EditUserPasswordWindowCommand()
        {
            EditUserWindow newEditUserWindow = new EditUserWindow();
            SetCenterPositionAndOpen(newEditUserWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

    }
}
