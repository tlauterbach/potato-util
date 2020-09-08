# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2020-09-08
### Added
- Dependencies to `com.potatointeractive.pcg` and `com.beauprime.beaudata` packages
- Simple `Pool<T>` data structure
- `FNVHash` struct based on the [Foller-Noll-Vo](http://www.isthe.com/chongo/tech/comp/fnv/) hash function
- An extension of PCG randomizer called `Randomizer` that is compatible with `BeauData` serialization
- `Base32` struct that can convert `ulong` integers into base32 strings
- A string window struct called `SubString` that points to a reference string instead of making a copy of it (useful for avoiding unessesary garbage collection).