using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoviesAfisha.ViewModels.UserViewModels
{
    public class AddOrderTicketViewModel:BaseViewModel
    {
        public static string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public static Tickets Ticket { get; set; }

        private RelayCommand addOrderTicket = null;
        public RelayCommand AddOrderTickett
        {
            get
            {
                return addOrderTicket ?? new RelayCommand(obj =>
                {
                    try
                    {
                        Window window = obj as Window;
                        string resultStr = "";

                        if (Name == null || Name.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControll(window, "NameBox");
                        }
                        else if (Surname == null || Surname.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControll(window, "SurnameBox");
                        }
                        else
                        {
                            UnitOfWork unitOfWork = new UnitOfWork();
                            User user = unitOfWork.User.GetAll().FirstOrDefault(el => el.Username == Username);

                            OrderTickets newOrderTicket = new OrderTickets
                            {
                                Name = this.Name,
                                Surname = this.Surname,
                                IdTicket = Ticket.Id,
                                IdUser = user.Id
                            };
                            resultStr = unitOfWork.OrderTicket.Create(newOrderTicket);

                            Tickets orderTicket = unitOfWork.Ticket.GetAll().FirstOrDefault(el => el.Id == Ticket.Id);
                            if (orderTicket != null)
                            {
                                orderTicket.Id = Ticket.Id;
                                orderTicket.IDSession = Ticket.IDSession;
                                orderTicket.Place = Ticket.Place;
                                orderTicket.Price = Ticket.Price;
                                orderTicket.Row = Ticket.Row;
                                orderTicket.Availability = false;
                                unitOfWork.OrderTicket.Save();
                                resultStr = "Билет заказан";
                            }

                            MessageWindowCommand.ShowMessageToUser(resultStr);
                            window.Close();
                        }
                    }
                    catch(Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
