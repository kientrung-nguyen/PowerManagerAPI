using System;
using System.Collections.Generic;

namespace PowerManagerAPI
{
    public enum ErrorCode : uint
    {
        SUCCESS = 0x000,
        FILE_NOT_FOUND = 0x002,
        ERROR_INVALID_PARAMETER = 0x057,
        ERROR_ALREADY_EXISTS = 0x0B7,
        MORE_DATA = 0x0EA,
        NO_MORE_ITEMS = 0x103
    }

    public enum AccessFlags : uint
    {
        ACCESS_SCHEME = 16,
        ACCESS_SUBGROUP = 17,
        ACCESS_INDIVIDUAL_SETTING = 18
    }

    public enum SettingSubgroup
    {
        NONE_SUBGROUP,
        DISK_SUBGROUP,
        BUTTONS_SUBGROUP,
        PROCESSOR_SUBGROUP,
        VIDEO_SUBGROUP,
        BATTERY_SUBGROUP,
        SLEEP_SUBGROUP,
        ENERGY_SAVER_SUBGROUP,
        GRAPHICS_SUBGROUP,
        IDLE_RESILIENCY_SUBGROUP,
        INTERNET_EXPL_SUBGROUP,
        INT_STEERING_SUBGROUP,
        MULTIMEDIA_SUBGROUP,
        PCIEXPRESS_SUBGROUP,
        DESKTOP_BACKGROUND_SUBGROUP,
        PRESENCE_AWARE_SUBGROUP,
        USB_SUBGROUP,
        WIRELESS_ADAPTER_SUBGROUP
    }

    public enum Setting
    {
        BATACTIONCRIT,
        BATACTIONLOW,
        BATFLAGSLOW,
        BATLEVELCRIT,
        BATLEVELLOW,
        LIDACTION,
        PBUTTONACTION,
        SBUTTONACTION,
        UIBUTTON_ACTION,
        DISKIDLE,
        ASPM,
        PROCFREQMAX,
        PROCFREQMAX1,
        PROCTHROTTLEMAX,
        PROCTHROTTLEMAX1,
        PROCTHROTTLEMIN,
        PROCTHROTTLEMIN1,
        PERFBOOSTMODE,
        CPMINCORES,
        CPMAXCORES,
        CPMINCORES1,
        CPMAXCORES1,
        PERFEPP,
        PERFEPP1,
        SYSCOOLPOL,
        HIBERNATEIDLE,
        HYBRIDSLEEP,
        GPUPREFERENCEPOLICY,
        RTCWAKE,
        STANDBYIDLE,
        ADAPTBRIGHT,
        VIDEOIDLE
    }

    public static class SettingIdLookup
    {
        public static Dictionary<SettingSubgroup, Guid> SettingSubgroupGuids = new Dictionary<SettingSubgroup, Guid>
        {
            { SettingSubgroup.NONE_SUBGROUP,                new Guid("fea3413e-7e05-4911-9a71-700331f1c294") }, // Settings belonging to no subgroup
            { SettingSubgroup.DISK_SUBGROUP,                new Guid("0012ee47-9041-4b5d-9b77-535fba8b1442") }, // Hard Disk
            { SettingSubgroup.BUTTONS_SUBGROUP,             new Guid("4f971e89-eebd-4455-a8de-9e59040e7347") }, // Power buttons and lid
            { SettingSubgroup.PROCESSOR_SUBGROUP,           new Guid("54533251-82be-4824-96c1-47b60b740d00") }, // Processor power management
            { SettingSubgroup.VIDEO_SUBGROUP,               new Guid("7516b95f-f776-4464-8c53-06167f40cc99") },
            { SettingSubgroup.BATTERY_SUBGROUP,             new Guid("e73a048d-bf27-4f12-9731-8b2076e8891f") },
            { SettingSubgroup.SLEEP_SUBGROUP,               new Guid("238C9FA8-0AAD-41ED-83F4-97BE242C8F20") },
            { SettingSubgroup.ENERGY_SAVER_SUBGROUP,        new Guid("de830923-a562-41af-a086-e3a2c6bad2da") },
            { SettingSubgroup.GRAPHICS_SUBGROUP,            new Guid("5fb4938d-1ee8-4b0f-9a3c-5036b0ab995c") },
            { SettingSubgroup.IDLE_RESILIENCY_SUBGROUP,     new Guid("2e601130-5351-4d9d-8e04-252966bad054") },
            { SettingSubgroup.INTERNET_EXPL_SUBGROUP,       new Guid("02f815b5-a5cf-4c84-bf20-649d1f75d3d8") },
            { SettingSubgroup.INT_STEERING_SUBGROUP,        new Guid("48672f38-7a9a-4bb2-8bf8-3d85be19de4e") }, // Interrupt Steering Settings
            { SettingSubgroup.MULTIMEDIA_SUBGROUP,          new Guid("9596fb26-9850-41fd-ac3e-f7c3c00afd4b") },
            { SettingSubgroup.PCIEXPRESS_SUBGROUP,          new Guid("501a4d13-42af-4429-9fd1-a8218c268e20") },
            { SettingSubgroup.DESKTOP_BACKGROUND_SUBGROUP,  new Guid("0d7dbae2-4294-402a-ba8e-26777e8488cd") },
            { SettingSubgroup.PRESENCE_AWARE_SUBGROUP,      new Guid("0d7dbae2-4294-402a-ba8e-26777e8488cd") },
            { SettingSubgroup.USB_SUBGROUP,                 new Guid("2a737441-1930-4402-8d77-b2bebba308a3") },
            { SettingSubgroup.WIRELESS_ADAPTER_SUBGROUP,    new Guid("19cbb8fa-5279-450e-9fac-8a3d5fedd0c1") }
        };

        public static Dictionary<Setting, Guid> SettingGuids = new Dictionary<Setting, Guid>
        {
            { Setting.BATACTIONCRIT,        new Guid("637ea02f-bbcb-4015-8e2c-a1c7b9c0b546") },
            { Setting.BATACTIONLOW,         new Guid("d8742dcb-3e6a-4b3c-b3fe-374623cdcf06") },
            { Setting.BATFLAGSLOW,          new Guid("bcded951-187b-4d05-bccc-f7e51960c258") },
            { Setting.BATLEVELCRIT,         new Guid("9a66d8d7-4ff7-4ef9-b5a2-5a326ca2a469") },
            { Setting.BATLEVELLOW,          new Guid("8183ba9a-e910-48da-8769-14ae6dc1170a") },
            { Setting.LIDACTION,            new Guid("5ca83367-6e45-459f-a27b-476b1d01c936") },
            { Setting.PBUTTONACTION,        new Guid("7648efa3-dd9c-4e3e-b566-50f929386280") },
            { Setting.SBUTTONACTION,        new Guid("96996bc0-ad50-47ec-923b-6f41874dd9eb") },
            { Setting.UIBUTTON_ACTION,      new Guid("a7066653-8d6c-40a8-910e-a1f54b84c7e5") },
            { Setting.DISKIDLE,             new Guid("6738e2c4-e8a5-4a42-b16a-e040e769756e") },
            { Setting.ASPM,                 new Guid("ee12f906-d277-404b-b6da-e5fa1a576df5") },
            { Setting.PROCFREQMAX,          new Guid("75b0ae3f-bce0-45a7-8c89-c9611c25e100") },
            { Setting.PROCFREQMAX1,         new Guid("75b0ae3f-bce0-45a7-8c89-c9611c25e101") },
            { Setting.PROCTHROTTLEMAX,      new Guid("bc5038f7-23e0-4960-96da-33abaf5935ec") },
            { Setting.PROCTHROTTLEMIN,      new Guid("893dee8e-2bef-41e0-89c6-b55d0929964c") },
            { Setting.PROCTHROTTLEMAX1,     new Guid("bc5038f7-23e0-4960-96da-33abaf5935ed") },
            { Setting.PROCTHROTTLEMIN1,     new Guid("893dee8e-2bef-41e0-89c6-b55d0929964d") },
            { Setting.PERFBOOSTMODE,        new Guid("be337238-0d82-4146-a960-4f3749d470c7") },
            { Setting.PERFEPP,              new Guid("36687f9e-e3a5-4dbf-b1dc-15eb381c6863") },
            { Setting.PERFEPP1,             new Guid("36687f9e-e3a5-4dbf-b1dc-15eb381c6864") },
            { Setting.CPMINCORES,           new Guid("0cc5b647-c1df-4637-891a-dec35c318583") },
            { Setting.CPMINCORES1,          new Guid("0cc5b647-c1df-4637-891a-dec35c318584") },
            { Setting.CPMAXCORES,           new Guid("ea062031-0e34-4ff1-9b6d-eb1059334028") },
            { Setting.CPMAXCORES1,          new Guid("ea062031-0e34-4ff1-9b6d-eb1059334029") },
            { Setting.SYSCOOLPOL,           new Guid("94d3a615-a899-4ac5-ae2b-e4d8f634367f") },
            { Setting.HIBERNATEIDLE,        new Guid("9d7815a6-7ee4-497e-8888-515a05f02364") },
            { Setting.HYBRIDSLEEP,          new Guid("94ac6d29-73ce-41a6-809f-6363ba21b47e") },
            { Setting.RTCWAKE,              new Guid("bd3b718a-0680-4d9d-8ab2-e1d2b4ac806d") },
            { Setting.STANDBYIDLE,          new Guid("29f6c1db-86da-48c5-9fdb-f2b67b1f44da") },
            { Setting.ADAPTBRIGHT,          new Guid("fbd9aa66-9553-4097-ba44-ed6e9d65eab8") },
            { Setting.VIDEOIDLE,            new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e") },
            { Setting.GPUPREFERENCEPOLICY,  new Guid("dd848b2a-8a5d-4451-9ae2-39cd41658f6c") } // 000 - None, 001 - Low Power
        };
    }
}