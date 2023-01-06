using System;
using System.Collections.Generic;
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

namespace AutoCompleteTest
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private ViewModel _vm ;

    public MainWindow()
    {
      InitializeComponent ();
      _vm = new ViewModel ();
      DataContext = _vm;
    }

    private void QueryBox_Populating( object sender, PopulatingEventArgs e )
    {
      _vm.Populate ();
      //QueryBox.PopulateComplete ();
    }

    private void QueryBox_DropDownClosing( object sender, RoutedPropertyChangingEventArgs<bool> e )
    {
      _vm.SelectedWord = _vm.CurrentWord;
    }
  }
}
