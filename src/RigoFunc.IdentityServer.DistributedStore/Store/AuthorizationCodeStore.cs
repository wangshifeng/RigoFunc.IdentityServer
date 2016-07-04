﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace RigoFunc.IdentityServer.DistributedStore {
    public class AuthorizationCodeStore : IAuthorizationCodeStore {
        private readonly IDistributedCache _cache;
        private readonly IDataSerializer<AuthorizationCode> _serializer;

        public AuthorizationCodeStore(IDistributedCache cache, IDataSerializer<AuthorizationCode> serializer) {
            _cache = cache;
            _serializer = serializer;
        }

        public async Task StoreAsync(string key, AuthorizationCode value) {
            var data = _serializer.Serialize(value);

            await _cache.SetAsync(key, data);
        }

        public async Task<AuthorizationCode> GetAsync(string key) {
            var data = await _cache.GetAsync(key);

            return _serializer.Deserialize(data);
        }

        public async Task RemoveAsync(string key) {
            await _cache.RemoveAsync(key);
        }

        public Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject) {
            throw new NotImplementedException();
        }

        public Task RevokeAsync(string subject, string client) {
            throw new NotImplementedException();
        }
    }
}
