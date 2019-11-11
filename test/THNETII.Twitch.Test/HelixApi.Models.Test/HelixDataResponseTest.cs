using System.Linq;
using Newtonsoft.Json;

using Xunit;

namespace THNETII.Twitch.HelixApi.Models.Test
{
    public static class HelixDataResponseTest
    {
        [Fact]
        public static void Can_deserialize_empty_object()
        {
            string json = JsonConvert.SerializeObject(new object());

            var dataResponse = JsonConvert.DeserializeObject<HelixDataResponse<object>>(json);

            Assert.NotNull(dataResponse);
            Assert.Empty(dataResponse.Data);
            Assert.Null(dataResponse.Pagination);
        }

        [Fact]
        public static void Can_deserialize_number_values()
        {
            int[] values = Enumerable.Range(0, 10).ToArray();
            string json = JsonConvert.SerializeObject(new
            {
                data = values
            });

            var dataResponse = JsonConvert.DeserializeObject<HelixDataResponse<int>>(json);

            Assert.NotNull(dataResponse);
            Assert.Equal(values, dataResponse.Data);
            Assert.Null(dataResponse.Pagination);
        }
    }
}
