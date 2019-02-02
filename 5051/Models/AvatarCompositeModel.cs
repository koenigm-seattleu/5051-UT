
namespace _5051.Models
{
    public class AvatarCompositeModel
    {
        // TODO, Mike.  Need to add ID here, and then fix up the existing data

        public string AvatarHeadUri;
        public string AvatarAccessoryUri;
        public string AvatarCheeksUri;
        public string AvatarExpressionUri;
        public string AvatarHairFrontUri;
        public string AvatarHairBackUri;
        public string AvatarShirtFullUri;
        public string AvatarShirtShortUri;
        public string AvatarPantsUri;

        public string HeadUri;
        public string AccessoryUri;
        public string CheeksUri;
        public string ExpressionUri;
        public string HairFrontUri;
        public string HairBackUri;
        public string ShirtFullUri;
        public string ShirtShortUri;
        public string PantsUri;

        public string HeadId;
        public string ShirtShortId;
        public string ShirtFullId;
        public string AccessoryId;
        public string CheeksId;
        public string ExpressionId;
        public string HairFrontId;
        public string HairBackId;
        public string PantsId;

        public string AvatarBase = "/Content/Avatar/";

        public AvatarCompositeModel()
        {
            var item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Head);

            if (item != null)
            {
                HeadId = item.Id;
                HeadUri = item.Uri;
                AvatarHeadUri = AvatarBase + HeadUri;
            }

            //item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtShort);
            //ShirtShortId = item.Id;
            //ShirtShortUri = item.Uri;
            //AvatarShirtShortUri = AvatarBase + ShirtShortUri;

            // Short Shirt, is the same as Long, just add Short to the URL
            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtFull);

            if (item != null)
            {
                ShirtFullId = item.Id;
                ShirtFullUri = item.Uri;
                AvatarShirtFullUri = AvatarBase + ShirtFullUri;

                // Short Shirt is the _short before the end of the file
                ShirtShortId = item.Id;
                var temp = item.Uri.Split('.');
                ShirtShortUri = temp[0] + "_short.png"; ;
                AvatarShirtShortUri = AvatarBase + ShirtShortUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);

            if (item != null)
            {
                AccessoryId = item.Id;
                AccessoryUri = item.Uri;
                AvatarAccessoryUri = AvatarBase + AccessoryUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Cheeks);

            if (item != null)
            {
                CheeksId = item.Id;
                CheeksUri = item.Uri;
                AvatarCheeksUri = AvatarBase + CheeksUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Expression);

            if (item != null)
            {
                ExpressionId = item.Id;
                ExpressionUri = item.Uri;
                AvatarExpressionUri = AvatarBase + ExpressionUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairFront);

            if (item != null)
            {
                HairFrontId = item.Id;
                HairFrontUri = item.Uri;
                AvatarHairFrontUri = AvatarBase + HairFrontUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairBack);

            if (item != null)
            {
                HairBackId = item.Id;
                HairBackUri = item.Uri;
                AvatarHairBackUri = AvatarBase + HairBackUri;
            }

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Pants);

            if (item != null)
            {
                PantsId = item.Id;
                PantsUri = item.Uri;
                AvatarPantsUri = AvatarBase + PantsUri;
            }
        }
    }
}