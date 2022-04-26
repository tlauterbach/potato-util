# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.8.1] - 2022-04-26
### Fixed
- missing `.meta` files for new Service Provider files

## [2.8.0] - 2022-04-26
### Added
- `ServiceCache`, `IService`, and `ServiceBehaviour` for use in service patterns

## [2.7.0] - 2022-04-25
### Added
- `GetCount` function for retreiving item count in a `Database`

## [2.6.0] - 2022-04-25
### Added
- `OnTaken` and `OnReleased` events to `Pool<T>`

## [2.5.1] - 2022-04-24
### Fixed
- issue where `Database` was not properly storing newly created items

## [2.5.0] - 2022-04-23
### Added
- `AddRange` extension for `HashSet<T>`
- `RemoveRange` extension for `HashSet<T>` and `IList<T>`

## [2.4.0] - 2022-04-23
### Added
- `Database` now requires a generic `IEquatable<FNVHash>` key type for identifying items stored
- missing `object` equality comparisons for `uint` and `int` in `FNVHash`

## [2.3.0] - 2022-04-22
### Added
- `FNVString` that is a version of the `FNVHash` that maintains the string value and uses it for serialization
- restored `FNVHash`'s ability to serialize itself as an `uint`

## [2.2.0] - 2022-04-20
### Added
- `Database` data structure that stores data by `FNVHash`
- `HashLookup` stores `string` representations of `FNVHash` for later lookup (can be disabled)
### Changed
- reduced scope of `FNVHash` so that it is no longer serializable or created from `uint` or `int`

## [2.1.0] - 2022-04-18
### Added
- developer can implicitly convert `string` to `Message`
- developer can clear entire `Messenger` with call to `DeregisterAll`
### Fixed
- issue where clearing `Messenger` would fail to do type conversion

## [2.0.0] - 2022-04-18
### Changed
- `Messenger` system now uses simplified `Message` struct instead of class, removed use of `Message<T>` in favor of struct

## [1.14.0] - 2021-08-27
### Added
- `FNVHash` can be implicitly created from a `string`, `int`, or `uint`

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