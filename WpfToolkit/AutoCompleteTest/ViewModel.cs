using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoCompleteTest
{
  internal class ViewModel : ObservableObject
  {
    private string                    _currentPrefix = null ;
    private List<WordDefViewModel>    _wordDefList   = null ;
    private WordDefViewModel          _currentWord   = null ;
    private WordDefViewModel          _selectedWord  = null ;

    public string PopulateText { get; set; }

    public List<WordDefViewModel> WordDefList { get => _wordDefList; set => SetProperty ( ref _wordDefList, value ); }
    public WordDefViewModel       CurrentWord { get => _currentWord; set => SetProperty ( ref _currentWord, value ); }
    public WordDefViewModel       SelectedWord { get => _selectedWord; set => SetProperty ( ref _selectedWord, value ); }
    public int                    WordCount => WordDefList == null ? 0 : WordDefList.Count ;

    public void Populate()
    {
      // Using MAXRECORDS = 1000, it takes a long time to show the drop down list for the first time.
      // After that, it fills much more quickly. I cannot figure out why this is, and it appeart to
      // be a WPF effect, and not a problem in the AutoCompleteBox.
      // Using MAXRECORDS = 100, the performance is better, but it requires more round trips to the
      // data source.
      const int MAXRECORDS = 100 ;

      if ( ( string.IsNullOrEmpty ( _currentPrefix ) )
           ||
           ( !PopulateText.StartsWith ( _currentPrefix ) )
           ||
           ( WordDefList.Count >= MAXRECORDS ) )
      {
        string url           = "https://api.datamuse.com" ;
        string urlParameters = $"words?sp={PopulateText}*&max={MAXRECORDS}";

        using ( var client = new HttpClient () )
        {
          client.BaseAddress = new Uri ( url );

          // Add an Accept header for JSON format.
          client.DefaultRequestHeaders.Accept.Add ( new MediaTypeWithQualityHeaderValue ( "application/json" ) );

          // Get data response
          var response = client.GetAsync(urlParameters).Result;

          if ( response.IsSuccessStatusCode )
          {
            // Parse the response body
            WordDefList = response.Content.ReadAsAsync<List<WordDefViewModel>> ().Result;
            OnPropertyChanged ( nameof( WordCount ) );

            // Finally store the prefix used to generate the list
            _currentPrefix = PopulateText;
          }
          else
          {
            Console.WriteLine ( "{0} ({1})", (int)response.StatusCode,
                          response.ReasonPhrase );
          }
        }
      }
    }

  }
}
