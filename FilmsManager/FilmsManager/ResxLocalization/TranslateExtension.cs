using FilmsManager.Resources;
using Models.Resources;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilmsManager.ResxLocalization
{
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		public static CultureInfo CultureInfo { get; set; }
		public static string ResourceId;

		static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
			() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

		public string Text { get; set; }

		public TranslateExtension()
		{
			CultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			ResourceId = $"{typeof(AppResources).GetTypeInfo().Namespace}.{nameof(AppResources)}";
		}

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Text == null)
				return string.Empty;

			var translation = ResMgr.Value.GetString(Text, CultureInfo);
			if (translation == null)
			{
			#if DEBUG
				throw new ArgumentException(
					string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, CultureInfo.Name),
					"Text");
			#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
			#endif
			}
			return translation;
		}
	}
}
