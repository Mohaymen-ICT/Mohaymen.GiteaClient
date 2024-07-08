using FluentAssertions;
using Mohaymen.GitClient.Tests.Mocks;
using Mohaymen.GiteaClient.APICall.Business.Serialization;
using Mohaymen.GiteaClient.APICall.Business.Serialization.Abstractions;
using Xunit;

namespace Mohaymen.GitClient.Tests.ApiCall.Business.Serialization;

public class JsonSerializerTests
{
    private readonly IJsonSerializer _sut;

    public JsonSerializerTests()
    {
        _sut = new JsonSerializer();
    }

    [Fact]
    public void Serialize_ShouldReturnSerializedJson_WhenInputObjectIsProvided()
    {
        // Arrange
        var inputObject = new FakeRequestBody(){ Name = "alireza", Age = 23 };
        const string expected = "{\"Name\":\"alireza\",\"Age\":23}";
        
        // Act
        var actual = _sut.SerializeObject(inputObject);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Deserialize_ShouldReturnDeserializedObject_WhenSerializedJsonIsProvided()
    {
        // Arrange
        const string inputJson = "{\"Name\":\"alireza\",\"Age\":\"23\"}";
        var expected = new FakeRequestBody(){ Name = "alireza", Age = 23 };
        
        // Act
        var actual = _sut.DeserializeJson<FakeRequestBody>(inputJson);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}