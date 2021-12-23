using RestSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;

namespace WebStore.Services
{
    public static class PhotoEditorService
    {
        //https://www.photoroom.com/api/ - a391536da5a8fec6155aca831bd7faf935acb025 (В месяц 1000 картинок бесплатно)
        public static byte[] MakeBackgroundTrancparent(byte[] photo)
        {
            var client = new RestClient("https://sdk.photoroom.com/v1/segment");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-api-key", "a391536da5a8fec6155aca831bd7faf935acb025");
            request.AddFile("image_file", photo, "myPicture");

            IRestResponse response = client.Execute(request);

            using Image image = Image.Load(response.RawBytes, new PngDecoder());
            image.Mutate(option => option.BackgroundColor(Color.Transparent));

            //.Remove(0, urlBase64String.IndexOf(',') + 1 - Мы убираем лишние символы для представления картинки как URL для HTML вставки в img src
            string urlBase64String = image.ToBase64String(Configuration.Default.ImageFormatsManager.FindFormatByFileExtension("png"));
            return Convert.FromBase64String(urlBase64String.Remove(0, urlBase64String.IndexOf(',') + 1));
        }
    }
}
