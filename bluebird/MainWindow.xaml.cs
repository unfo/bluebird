using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


// Provides access to the fluent API; required
using Dimebrain.TweetSharp.Fluent;

// Provides access to the data classes that represent Twitter results
using Dimebrain.TweetSharp.Model;

// Provides access to features like relative time, and casting from XML/JSON to
// Twitter data classes
using Dimebrain.TweetSharp.Extensions;

namespace bluebird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var user = "username";
            var password = "password";
           
            
            var result = FluentTwitter.CreateRequest()
                                       .AuthenticateAs(user, password)
                                       .Statuses().OnHomeTimeline().Request();


             
            if (!result.IsTwitterError)
            {
                
                var FriendTweets = (from tweet in result.AsStatuses()
                                select new
                                {
                                    CreatedDate = tweet.CreatedDate.ToString("dd.MM.yyyy hh:mm"),
                                    ScreenName = "@" + tweet.User.ScreenName,
                                    Text = tweet.Text
                                }).ToList();
                
                Binding bind = new Binding();
                listView1.DataContext = FriendTweets;
                listView1.SetBinding(ListView.ItemsSourceProperty, bind);
            }
            else
            {
                //DebugLog.Content += string.Format("Twitter Returned Error {0} - {1}", result.ResponseHttpStatusCode, result.ResponseHttpStatusDescription);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                listView1.Width = e.NewSize.Width - 40;
        }
    }
}
