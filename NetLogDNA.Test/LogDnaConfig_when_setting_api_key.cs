using System;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NetLogDNA.Test
{
    public class LogDnaConfig_when_setting_api_key
    {
        [Fact]
        public void Then_value_is_set_correctly()
        {
            // Arrange
            var apiKey = Guid.NewGuid().ToString();
            
            // Act
            LogDnaConfig.ApiKey = apiKey;
            
            // Assert
            LogDnaConfig.ApiKey.Should().Be(apiKey);
        }

        [Fact]
        public void Then_authorization_property_returns_correct_base64_basic_auth_header()
        {
            // Arrange
            var apiKey = Guid.NewGuid().ToString();
            var base64EncodedAuthKey = ConvertToBase64($"{apiKey}:");
            
            // Act
            LogDnaConfig.ApiKey = apiKey;
            
            // Assert
            LogDnaConfig.AuthorizationHeader.Should().NotBeNull();
            LogDnaConfig.AuthorizationHeader.Should().StartWith("Basic ");
            LogDnaConfig.AuthorizationHeader.Should().EndWith(base64EncodedAuthKey);
        }
        
        private static string ConvertToBase64(string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }
    }
}