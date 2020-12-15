using System;

namespace DblDip.Core
{
    public static class Constants
    {
        public static class Rates
        {
            public static readonly Guid PhotographyRate = new Guid("6af71681-ad24-4341-97e7-a0f654a395b1");
            public static readonly Guid TravelRate = new Guid("dd9d8c0e-da38-4b21-a62a-4a1b0505db5f");
            public static readonly Guid ConsulationRate = new Guid("35ab4b3a-2409-47be-a797-991859aa1b36");
        }

        public static class Roles
        {
            public static readonly Guid Participant = new Guid("43853d94-a615-4f58-bb02-4a46eb60b1df");
            public static readonly Guid Lead = new Guid("d080406c-b3b8-4ec1-b41a-3062f75e9153");
            public static readonly Guid Client = new Guid("ff8bf475-567c-4daa-b509-a1c23e7ae78d");
            public static readonly Guid Photographer = new Guid("da296e6d-00cb-4978-8d82-1820133b6c1e");
            public static readonly Guid ProjectManager = new Guid("7d8f10af-024e-4b4b-8476-be54997041de");
            public static readonly Guid SystemAdministrator = new Guid("9a1481ba-9bfa-4d40-949d-df365713e5bc");
        }

        public static class ClaimTypes
        {
            public static readonly string UserId = nameof(UserId);
            public static readonly string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        }

        public static class Surveys
        {
            public static readonly Guid Satisfaction = new Guid("99e1d8d1-5084-4228-8be5-d723921e3dce");
        }

        public static class Photographers
        {
            public static readonly Guid QuinntyneBrown = new Guid("5cb425e4-0a41-4cfa-a6b8-ba3ac84c14ce");
        }

        public static class WeddingShots
        {
            public static readonly string FlatLay = nameof(FlatLay);
        }

        public static class Services
        {
            public static readonly string Wedding = nameof(Wedding);
            public static readonly string Engagement = nameof(Engagement);
            public static readonly string FamilyPortrait = nameof(FamilyPortrait);
            public static readonly string EditedPhoto = nameof(EditedPhoto);
            public static readonly string Portrait = nameof(Portrait);
            public static readonly string CorporateEvent = nameof(CorporateEvent);
            public static readonly string SocialEvent = nameof(SocialEvent);
            public static readonly string StudioPortrait = nameof(StudioPortrait);
        }

        public static class PhotoGalleries
        {
            public static readonly Guid MyImages = Guid.NewGuid();
        }

        public static class Libaries
        {
            public static readonly Guid MyFiles = Guid.NewGuid();
        }

        public static class ConfigurationKeys
        {
            public static readonly string DataDefaultConnectionString = "Data:DefaultConnection:ConnectionString";
        }
    }
}
