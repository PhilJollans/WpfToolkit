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
    private List<WordDefViewModel>    _wordDefList ;
    private WordDefViewModel          _currentWord   = null ;
    private WordDefViewModel          _selectedWord  = null ;

    public string PopulateText { get; set; }

    public List<WordDefViewModel> WordDefList { get => _wordDefList; set => SetProperty ( ref _wordDefList, value ); }
    public WordDefViewModel CurrentWord { get => _currentWord; set => SetProperty ( ref _currentWord, value ); }
    public WordDefViewModel SelectedWord { get => _selectedWord; set => SetProperty ( ref _selectedWord, value ); }

    public void Populate()
    {
      const int MAXRECORDS = 1000 ;

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
