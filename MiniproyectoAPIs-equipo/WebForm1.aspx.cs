using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace MiniproyectoAPIs_equipo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private const string ApiKey = "AIzaSyB_t16m11k1i7x1qKcBs8FtQnjem1z0gd4"; // Reemplaza con tu clave de API

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializa la página si es necesario
            }
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text; // Supone que tienes un TextBox llamado txtSearch
            var videos = await SearchVideosAsync(query);
            DisplayResults(videos);
        }

        private async Task<List<YouTubeVideo>> SearchVideosAsync(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = ApiKey,
                ApplicationName = "YouTubeApiExample"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = query;
            searchRequest.MaxResults = 10;
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;

            var searchResponse = await searchRequest.ExecuteAsync();
            var videos = new List<YouTubeVideo>();

            foreach (var item in searchResponse.Items)
            {
                if (item.Id.Kind == "youtube#video")
                {
                    videos.Add(new YouTubeVideo
                    {
                        Title = item.Snippet.Title,
                        Url = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                        ThumbnailUrl = item.Snippet.Thumbnails.Default__.Url // Agrega la URL de la miniatura
                    });
                }
            }
            return videos;
        }

        private void DisplayResults(List<YouTubeVideo> videos)
        {
            resultsPanel.Controls.Clear(); // Limpia el panel de resultados

            foreach (var video in videos)
            {
                // Crear un contenedor para cada video
                var videoItem = new Panel { CssClass = "video-item" };

                // Agregar la miniatura
                var thumbnail = new System.Web.UI.WebControls.Image
                {
                    ImageUrl = video.ThumbnailUrl,
                    AlternateText = video.Title,
                    Width = 120,
                    Height = 90
                };

                // Agregar un link al video
                var link = new System.Web.UI.WebControls.HyperLink
                {
                    NavigateUrl = video.Url,
                    Text = video.Title,
                    Target = "_blank" // Abre el enlace en una nueva pestaña
                };

                // Agregar controles al contenedor
                videoItem.Controls.Add(thumbnail);
                videoItem.Controls.Add(link);

                // Agregar el contenedor al panel de resultados
                resultsPanel.Controls.Add(videoItem);
            }
        }
    }

    public class YouTubeVideo
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; } // Agrega la propiedad para la miniatura
    }
}