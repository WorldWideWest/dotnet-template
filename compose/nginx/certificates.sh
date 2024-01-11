#!/bin/bash

mkdir -p keys
mkdir -p certs

openssl genpkey \
    -algorithm RSA \
    -out keys/identity.provider.com.key

openssl req \
    -new -x509 -key keys/identity.provider.com.key \
    -out certs/identity.provider.com.crt \
    -days 365 \
    -subj "/CN=identity.provider.com" \
    -addext "subjectAltName=DNS:identity.provider.com"
