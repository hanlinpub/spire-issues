# Summary

This project reproduces issue #1: https://github.com/hanlinpub/spire-issues/issues/1

Sample test outputs can be found in `/output-samples`.

# Running the test

### Command line

```
dotnet test -v n
```

### Docker

Note: enable BuildKit to utilize custom build outputs feature.

See: https://docs.docker.com/engine/reference/commandline/build/#custom-build-outputs

```
# Enable BuildKit - PowerShell example
$env:DOCKER_BUILDKIT=1

docker build -o output .
```