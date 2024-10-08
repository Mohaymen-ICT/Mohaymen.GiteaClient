﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;

internal record FileCommitRequest
{
    [JsonProperty("path")]
    public required string Path { get; init; }
    
    [JsonProperty("content")]
    public required string Content { get; set; }
    
    [JsonProperty("sha")]
    public string? FileHash { get; init; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("operation")]
    public required CommitAction CommitAction { get; init; }
}

internal enum CommitAction
{
    [EnumMember(Value = "create")]
    Create,
    [EnumMember(Value = "update")]
    Update,
    [EnumMember(Value = "delete")]
    Delete
}
