# Copilot Instructions

## Shared Instructions

Shared Copilot instructions, skills and prompts are maintained centrally in the
[account-level `.github` repository](https://github.com/f2calv/.github). They are deliberately not
copied here.

To load them, clone that repository and either add it to the VS Code workspace or link its
instruction, skill and prompt folders into `~/.copilot/`. If those files are unavailable, stop
rather than guessing the conventions.

Everything below is specific to this repository.

## Repository Purpose

This repository is an ASP.NET Core SignalR playground. `WebApp` hosts the hub, the two client
applications exercise it, and the client and shared libraries contain reusable protocol code.

- Update hub contracts and all clients together.
- Keep MessagePack and JSON protocol behavior interoperable.
- Use synthetic messages in examples, tests and diagnostics.
