# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]

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
