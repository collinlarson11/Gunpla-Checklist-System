# Gunpla-Checklist-System
Management system for Gunpla Kits 
classDiagram
    %% Inheritance: RealGradeKit IS-A GunplaKit
    GunplaKit <|-- RealGradeKit
    
    %% Composition: CollectionManager HAS-A List of GunplaKits
    CollectionManager "1" *-- "many" GunplaKit : manages

    class GunplaKit {
        +string ModelName
        +string Series
        +bool IsBuilt
        +MarkAsBuilt() void
        +GetDetails() string
    }

    class RealGradeKit {
        +int LineNumber
        +string Scale
        +GetDetails() string
    }

    class MasterGradeKit {
        +int LineNumber
        +string Scale
        +GetDetails() string
    }

    class CollectionManager {
        +List~GunplaKit~ MyKits
        +AddKit(GunplaKit kit) void
        +DisplayChecklist() void
        +GetCollectionStats() string
    }

    class Program {
        <<Main>>
        +Main() void
        +ShowMenu() void
    }
