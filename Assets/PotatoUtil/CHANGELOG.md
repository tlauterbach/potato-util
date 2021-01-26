# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.7.0] - 2021-01-26
### Added
- `IndexedSet<T>` now has a function for polling the set for containing an item (`bool Contains(T item)`)
- acknowledgement that PotatoUtil is available on [openupm.com](https://openupm.com)

### Changed
- `IndexedSet<T>` now requires its generic type implement `IEquatable<T>` so that checks can compare equality

## [1.6.0] - 2021-01-22
### Added
- `WrappedAttribute` and `WrappedAttributeDrawer` to draws an Inspector/Editor `string` field as wrapped instead of simply overflowing the box width (which is the default unity behaviour).

## [1.5.0] - 2021-01-14
### Added
- `NonDrawingGraphic` and `NonDrawingGraphicEditor` courtesy of Slipp Douglas Thompson's [Gist](https://gist.github.com/capnslipp/349c18283f2fea316369).
- `PotatoUtil.Editor` namespace for Editor only scripts and utilities.

## [1.4.1] - 2021-01-02
### Fixed
- `Base32` defect where `Parse` would always fail with correct length strings

## [1.4.0] - 2020-12-21
### Changed
- `Singleton<T>` now has its `IsNull` property exposed as public so whether or not it is active can be polled by developer implementations.

## [1.3.1] - 2020-12-08
### Added
- `Singleton<T>` components now have inspector option to destroy only the component or the entire game object when a singleton is destroyed because one already exists.

## [1.3.0] - 2020-12-05
### Added
- `Singleton<T>` class that implements common singleton behaviour with MonoBehaviour. Access to the singleton itself is dictated by classes that implement `Singleton<T>`

## [1.2.1] - 2020-10-16
### Fixed
- Dependencies are now listed in the `package.json`

## [1.2.0] - 2020-10-16
### Added
- New struct `MinMax` that allows a developer to have a serialized pair of floats indicating a minimum and maximum value and a `Clamp` function
- `MinMax` has Unity serialization and inspector implementation

## [1.1.0] - 2020-10-14
### Added
- New data structure `IndexedSet<T>` that implements `System.Collections.IReadOnly<T>` that doesn't have the option to cast to an `IList<T>` like `System.Collections.ObjectModel.ReadOnlyList<T>`. The new data structure was added to disallow the option to do the casting and explicity maintain the readonly state of the set by not even given the option to cast to `IList<T>`.

## [1.0.0] - 2020-09-08
### Added
- Dependencies to `com.potatointeractive.pcg` and `com.beauprime.beaudata` packages
- Simple `Pool<T>` data structure
- `FNVHash` struct based on the [Foller-Noll-Vo](http://www.isthe.com/chongo/tech/comp/fnv/) hash function
- An extension of PCG randomizer called `Randomizer` that is compatible with `BeauData` serialization
- `Base32` struct that can convert `ulong` integers into base32 strings
- A string window struct called `SubString` that points to a reference string instead of making a copy of it (useful for avoiding unessesary garbage collection).