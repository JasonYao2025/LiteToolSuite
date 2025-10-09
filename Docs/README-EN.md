# LiteToolSuite [中文](https://github.com/walker0012025/API-TestPilot/blob/main/README.md) |English

## 1. LiteToolSuite Overview

LiteToolSuite (Chinese name: ‌**Mini Toolkit Set**‌) is a collection of testing utilities developed by Jason with technical support. Designed to facilitate device integration, it aims to enhance hardware testing efficiency for software engineers and enable FAEs to troubleshoot issues rapidly.

## 2. LiteToolSuite Usage Guidelines

- Acknowledge the project source when used.
- ‌**Vision**‌: Empower testers to work effortlessly. If you find this project valuable, a star is greatly appreciated.
- Authors, contributors, and affiliated parties assume no liability for direct or indirect damages caused by using this open-source project.
- ‌**Prohibited uses**‌: Illegal activities, violation of public morals, infringement of privacy/IPR/other rights, or scenarios posing risks to persons/property/environment.

## 3. LiteToolSuite Release History

- ‌**2025.10.10**‌: LiteToolSuite 1.0 officially opens source.

## 4. LiteToolSuite Development Goals

Originally conceived to enable MQTT command execution via Winform clicks for convenience, the toolkit later expanded to streamline testing workflows with:

- [x] Multilingual framework based on MVC architecture.
- [ ] MQTT message subscription/publishing.
- [x] WebAPI invocation examples.
- [x] Time/Timestamp calculations.
- [x] Comprehensive Helpers (e.g., SQLiteHelper) for extended development.

------

‌**Note**‌:

- Technical terms like "MQTT", "Winform", and "MVC" retain their original forms as they are standardized acronyms.
- Checkbox symbols ([x]/[ ]) are preserved for consistency with the original progress tracking format.
- Legal disclaimers are translated with formal phrasing to maintain contractual rigor.

## 5. LiteToolSuite Architecture

LiteToolSuite follows the ‌**MVC architecture**‌, structured as follows:

- ‌**Forms**‌: Contains UI, controls, and corresponding event handlers.

- ‌**Models**‌: Hosts entity classes for JSON file parsing.

- ‌**BLL (Business Logic Layer)**‌: Implements core business logic (e.g., fetching site vehicles as a `Dictionary`).

- ‌Common‌: Utility helpers like:`HttpClientHelper``LanguageHelper``RegexHelper``SecurityHelper``SQLiteHelper`

  `StringHelper``TimeHelper`(Shared across Forms and BLL.)

## 6. LiteToolSuite Contributors

- Currently maintained solely by the author. Community contributions are welcome.

## 7. LiteToolSuite Quick Start Guide

- ‌**Download‌**: git clone https://github.com/JasonYao2025/LiteToolSuite.git  
- ‌**Open Project**‌: Use ‌**VS2022**‌ to load the solution.
- ‌**Copy Files**: Copy `Docs/LiteToolSuite` and `Imgs` to the build directory.
- ‌**Run**‌: Compile and launch via VS2022.

‌**Note**‌:

- LiteToolSuite is a ‌**client-side tool**‌ requiring server-side WebAPI support.
- Users must adapt APIs/JSON formats for their own servers by:
  - Modifying API paths in code.
  - Creating custom entity classes.
  - Contacting the author if needed.

------

‌**Key Terms Retained**‌:

- MVC, BLL, JSON, WebAPI, VS2022 (standard technical terms).
- Code blocks and paths are preserved verbatim.
- Structured formatting (bullets, bolding) aligns with the original.

Let me know if you'd like any refinements!
