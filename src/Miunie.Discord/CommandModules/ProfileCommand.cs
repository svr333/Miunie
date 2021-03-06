using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand : BaseCommandModule
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly ProfileService _profileService;

        public ProfileCommand(EntityConvertor entityConvertor, ProfileService profileService)
        {
            _entityConvertor = entityConvertor;
            _profileService = profileService;
        }

        [Command("profile")]
        public async Task ShowProfileAsync(CommandContext ctx, MiunieUser m = null)
        {
            if (m is null)
            {
                m = _entityConvertor.ConvertUser(ctx.Member);
            }
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.ShowProfileAsync(m, channel);
        }

        [Command("+rep")]
        public async Task AddReputationAsync(CommandContext ctx, MiunieUser m)
        {
            var source = _entityConvertor.ConvertUser(ctx.Member);
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.GiveReputationAsync(source, m, channel);
        }

        [Command("-rep")]
        public async Task RemoveReputationAsync(CommandContext ctx, MiunieUser m)
        {
            var source = _entityConvertor.ConvertUser(ctx.Member);
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.RemoveReputationAsync(source, m, channel);
        }

        [Command("guild")]
        public async Task ShowGuildInfoAsync(CommandContext ctx)
        {
            var guild = _entityConvertor.ConvertGuild(ctx.Guild);
            var channel = _entityConvertor.ConvertChannel(ctx.Channel);
            await _profileService.ShowGuildProfileAsync(guild, channel);
        }
    }
}
