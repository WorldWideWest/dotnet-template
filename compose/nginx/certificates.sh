#!/bin/bash

mkdir -p keys
mkdir -p certs

openssl genpkey \
    -algorithm RSA \
    -out keys/localhost.key

openssl req \
    -new -x509 -key keys/localhost.key \
    -out certs/localhost.crt \
    -days 365 \
    -subj "/CN=localhost" \
    -addext "subjectAltName=DNS:localhost"
