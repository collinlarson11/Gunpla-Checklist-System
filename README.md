# Gunpla-Checklist-System
Management system for Gunpla Kits 
classDiagram
    direction TB

   %% Relationships
    GunplaKit <|-- RealGradeKit : Inheritance
    GunplaKit <|-- MasterGradeKit : Inheritance
    CollectionManager "1" *-- "many" GunplaKit : Composition (MyKits)
    Program ..> CollectionManager : Dependency (Uses)
    Program ..> GunplaKit : Dependency (Creates)

    class Program {
        +Main(args: string[]) void$
        +ShowMenu() void$
        -AddKitWorkflow(manager: CollectionManager) void$
        -Pause() void$
    }

    class CollectionManager {
        -List~GunplaKit~ MyKits
        -string DataFilePath$
        +CollectionManager()
        +AddKit(kit: GunplaKit) void
        +DisplayChecklist() void
        +GetCollectionStats() string
        +TryMarkKitBuilt(oneBasedIndex: int) bool
        +TryDeleteKit(oneBasedIndex: int) bool
        +DisplayUnbuiltKits() List~GunplaKit~
        +Save() void
        +Load() void
    }

    class GunplaKit {
        +string ModelName
        +bool IsBuilt
        +string Series
        +string Scale
        +string Grade
        +GunplaKit()
        +GunplaKit(name: string, series: string, scale: string)
        +MarkAsBuilt() void
        +GetDetails() string*
    }

    class RealGradeKit {
        +int LineNumber
        +RealGradeKit()
        +RealGradeKit(name: string, series: string, line: int, scale: string)
        +GetDetails() string
    }

    class MasterGradeKit {
        +MasterGradeKit()
        +MasterGradeKit(name: string, series: string, scale: string)
        +GetDetails() string
    }
