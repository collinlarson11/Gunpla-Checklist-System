# Gunpla-Checklist-System
Management system for Gunpla Kits 
classDiagram
    direction TB

    %% Relationships
    CollectionManager "1" *-- "many" GunplaKit : manages
    GunplaKit <|-- RealGradeKit : inheritance
    GunplaKit <|-- MasterGradeKit : inheritance
    Program ..> CollectionManager : uses

    class GunplaKit {
        +string ModelName
        +string Series
        +string Scale
        +bool IsBuilt
        +MarkAsBuilt() void
        +GetDetails() string*
    }

    class RealGradeKit {
        +int LineNumber
        +GetDetails() string
    }

    class MasterGradeKit {
        +int LineNumber
        +GetDetails() string
    }

    class CollectionManager {
        -List~GunplaKit~ MyKits
        -string DataFilePath
        +AddKit(GunplaKit kit) void
        +TryMarkKitBuilt(int index) bool
        +TryDeleteKit(int index) bool
        +DisplayChecklist() void
        +GetCollectionStats() string
        +Save() void
        +Load() void
    }

    class Program {
        <<Main>>
        +Main() void
        +ShowMenu() void
        -AddKitWorkflow() void
        -Pause() void
    }
