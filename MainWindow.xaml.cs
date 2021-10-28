using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MySQL_WPF_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string ip = "127.0.0.1";
        public static int port = 3306;
        public static string username = "root";
        public static string password = "";
        public static string database = "huita";
        public MySqlConnection conn = new MySqlConnection("Server=" + ip + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password);
        public static MySqlCommand cmd = new MySqlCommand();
        public static int counter;

        public MainWindow()
        {
            InitializeComponent();
            Logisn.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Collapsed;
        }

        private void Logins(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();

                cmd.Connection = conn;

                string sql = "Select * from users where Login Like '" + Loginn.Text + "' and Pass Like '" + Passs.Password + "'";
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                    try
                    {
                        if (reader.Read())
                        {
                            var result = MessageBox.Show("Правильно", "Login", MessageBoxButton.YesNoCancel, MessageBoxImage.Hand);
                            
                        }
                        else
                        {
                            MessageBox.Show("Не Правильно", "Login");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Logisn.Visibility = Visibility.Collapsed;
            Register.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Logisn.Visibility = Visibility.Visible;
            Register.Visibility = Visibility.Collapsed;
        }

        private void RegOnSys(object sender, RoutedEventArgs e)
        {

                if (pas1.Text == pas2.Text)
                {
                    try
                    {
                        conn.Open();

                        cmd.Connection = conn;

                        string sql = "Insert into users Values (NULL, '"+Loginns.Text+"', '"+pas1.Text+"')";
                        try 
                        { 
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        }
                        catch (MySqlException ex)
                        {
                        counter++;
                        if(counter < 3) MessageBox.Show("Имя занято, попробуйте другое.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        if(counter > 2 && counter < 5) MessageBox.Show("ИМЯ ЗАНЯТО","Регистрация",MessageBoxButton.OK,MessageBoxImage.Error);
                        if(counter > 4 && counter < 7) MessageBox.Show("ИМЯ ЗАНЯТО СУКА НЁБЕР ТЫ ЕБАНЫЙ", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (counter > 6) MessageBox.Show("Ты пидор не читери.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совподают!", "Регистрация",MessageBoxButton.OK,MessageBoxImage.Error);
                }

        }
    }
}
