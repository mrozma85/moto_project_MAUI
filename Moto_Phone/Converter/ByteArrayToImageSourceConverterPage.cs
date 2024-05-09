using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;

namespace Moto_Phone.Converter
{
    class ByteArrayToImageSourceConverterPage : ContentPage
    {
        //public ByteArrayToImageSourceConverterPage()
        //{
        //    Content = new Image()
        //        .Bind(
        //            Image.SourceProperty,
        //            static (ViewModel vm) => vm.DotNetBotImageByteArray,
        //            mode: BindingMode.OneWay,
        //            converter: new ByteArrayToImageSourceConverter());
        //}

        //public ByteArrayToImageSourceConverterPage()
        //{
        //    var image = new Image();

        //    image.SetBinding(
        //        Image.SourceProperty,
        //        new Binding(
        //            nameof(ViewModel.DotNetBotImageByteArray),
        //            mode: BindingMode.OneWay,
        //            converter: new ByteArrayToImageSourceConverter()));

        //    Content = image;
        //}
    }
}
