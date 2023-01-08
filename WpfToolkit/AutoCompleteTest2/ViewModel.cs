using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteTest2
{
  internal class ViewModel
  {
    public List<string> WordList1 { get; private set; }
    public List<string> WordList2 { get; private set; }
    public List<string> WordList3 { get; private set; }

    public ViewModel()
    {
      WordList1 = new List<string>();
      for ( int i = 0 ; i< 1000 ; i++ )
      {
        WordList1.Add ( $"A{i}" ) ;
      }

      WordList2 = new List<string> ();
      for ( int i = 0 ; i < 1000 ; i++ )
      {
        WordList2.Add ( $"A{i}" );
      }

      WordList3 = new List<string> ();
      for ( int i = 0 ; i < 100 ; i++ )
      {
        WordList3.Add ( $"A{i}" );
      }
    }

  }
}
