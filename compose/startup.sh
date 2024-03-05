dotnet watch run \
    -v \
    --project /app/src/Template.Api/Template.Api.csproj \
    --urls "http://*:5000" \
    --non-interactive \
    --disable-parallel
