// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Naming", "CA1716:Identifiers should not match keywords",
    Justification = "We will not be consuming Sorcery from VB...",
    Scope = "type",
    Target = "~T:Sorcery.ModularCourse.Module")]
