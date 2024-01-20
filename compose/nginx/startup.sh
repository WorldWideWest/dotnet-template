#!/bin/bash

mkdir -p keys
mkdir -p certs

openssl genpkey \
    -algorithm RSA \
    -out /etc/ssl/private/localhost.key

openssl req \
    -new -x509 -key /etc/ssl/private/localhost.key \
    -out /etc/ssl/certs/localhost.crt \
    -days 365 \
    -subj "/CN=localhost" \
    -addext "subjectAltName=DNS:localhost"

exec nginx -g 'daemon off;'