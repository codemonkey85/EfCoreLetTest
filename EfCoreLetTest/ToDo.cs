using System.ComponentModel.DataAnnotations;

namespace EfCoreLetTest;

public record ToDo([property: Key] long Id, string TaskTitle);
