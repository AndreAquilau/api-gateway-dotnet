﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure.Data.Models;

public class GenericPayload<TPayload>
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public string transactionId { get; set; } = string.Empty;
    public TPayload? payload { get; set; } 
}
