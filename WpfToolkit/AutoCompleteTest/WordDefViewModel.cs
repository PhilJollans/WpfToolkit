using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteTest
{
  internal class WordDefViewModel
  {
    public string   Word { get; set; }
    public int      Score { get; set; }
    public string[] Tags { get; set; }

    public override string ToString() => Word;
  }
}
