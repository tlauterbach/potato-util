# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.13.0] - 2021-05-30
### Changed
- Updated `unity-pcg-csharp` to version 1.2.0
- Updated `BeauData` to version 0.3.1

## [1.12.0] - 2021-05-20
### Changed
- `Priority` parameter for registering messages to `Messenger` is now optional, defaults to 0

## [1.11.0] - 2021-05-08
### Added
- `FNVHash` can now be serialized and edited from the Unity inspector

## [1.10.1] - 2021-05-03
### Fixed
- `PriorityQueue` is now properly part of the PotatoUtil namespace

## [1.10.0] - 2021-05-03
### Added
- `PriorityQueue<T,U>` data structure from [BlueRaja's High Speed Priority Queue](https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp) project

## [1.9.0] - 2021-05-02
### Added
- `uint` indexer getter to `IndexedSet<T>`
- `SceneReference` class that can serialize a string reference to a Scene Asset from the inspector and allow addition to the project's scene list

## [1.8.0] - 2021-05-02
### Added
- `Messenger` class that can have `Message` and `Message<T>` registered to it and broadcast
- `SortedAction` data structure that allows prioritization of actions registered to it upon invocation

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