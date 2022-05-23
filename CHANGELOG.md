# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [6.2.0] - 2022-05-23
### Added
 - UI Bindings for configurable binding

## [6.1.1] - 2021-05-14
### Changed
 - Themes ensure rooms are loaded before applying settings

## [6.1.0] - 2020-08-13
### Changed
 - UI Factory GetRooms method is virtual - Allows implementations to select rooms for binding

## [6.0.0] - 2020-06-22
### Changed
 - Fixed a bug on program stop where UI factories would attempt to lookup rooms
 - Common theme logic moved into abstraction

## [5.2.0] - 2020-10-06
### Changed
 - UIs are activated as part of lifecycle change
 - UI factories expose virtual GetRooms() method

## [5.1.1] - 2020-08-06
### Changed
 - Changed DgeX00 interfaces to new namespace
 - Removed references to CrestronPro projects

## [5.1.0] - 2020-06-30
### Added
 - Added Themes.Components project
 - Added StreamSwitcherEmbeddedVideoPresenterHelper to help with DGE-based switching

## [5.0.1] - 2020-04-29
### Changed
 - Relaxed AbstractTheme settings type contraint

## [5.0.0] - 2020-02-26
### Changed
 - Moved common UI factory implementation details into abstractions
 - Better handling of UI to Room bindings, particularly in room-combine systems
 - Fixed a bug where UIs would not have their room cleared when the room is part of a combine space

## [4.3.0] - 2019-09-16
### Added
 - Added default implementation for AbstractTheme ActiveUserInterfaces method

## [4.2.0] - 2019-06-07
### Added
 - Added abstractions and interfaces for UIs and UI Factories

## [4.1.0] - 2019-06-06
### Added
 - Add features for activating a user interface, indicating it is ready for human interaction

## [4.0.0] - 2018-05-24
### Removed
 - Removed element property from settings

## [3.0.0] - 2018-04-23
### Changed
 - Removed suffix from assembly name
 - Fixed missing reference
