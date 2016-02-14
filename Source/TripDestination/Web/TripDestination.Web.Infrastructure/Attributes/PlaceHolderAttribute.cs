namespace TripDestination.Web.Infrastructure.Attributes
{
    using System;
    using System.Web.Mvc;

    public class PlaceHolderAttribute : Attribute, IMetadataAware
    {
        private readonly string _placeholder;

        public PlaceHolderAttribute(string placeholder)
        {
            _placeholder = placeholder;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["placeholder"] = _placeholder;
        }
    }
}
