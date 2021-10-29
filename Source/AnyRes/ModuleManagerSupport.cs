/*
	This file is part of AnyRes /L Unleashed
		© 2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2017-2020 LinuxGuruGamer
		© 2015-2016 CriftonM (Crifton Marien)

	FShangarExtender is double licensed, as follows:

		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	AnyRes /L Unleashed is distributed in the hope that it will be
	useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with AnyRes /L Unleashed. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with AnyRes /L Unleashed. If not, see <https://www.gnu.org/licenses/>.

*/
using System.Collections.Generic;

namespace AnyRes
{
	public static class ModuleManagerSupport
	{
		public static IEnumerable<string> ModuleManagerAddToModList()
		{
			string[] r = { typeof(ModuleManagerSupport).Namespace };
			return r;
		}
	}
}
