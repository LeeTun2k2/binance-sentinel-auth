#!/bin/sh
# For running docker only

echo "Running database migrations..."
exec dotnet BinanceSential.Auth.Web.dll --migrate

echo "Starting the application..."
exec dotnet BinanceSential.Auth.Web.dll
