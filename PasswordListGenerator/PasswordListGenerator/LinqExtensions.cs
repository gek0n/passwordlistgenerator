// Copyright © 2016 Zagurskiy Mikhail. All rights reserved. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace PasswordListGenerator
{
	public static class LinqExtensions
	{
		/**
		 * Создает все возможные комбинации элементов переданной последовательности sequences.
		 * Если необходимы повторы, то надо передать true в функцию
		 */
		public static IEnumerable<IEnumerable<T>> Combinations<T>(
			this IEnumerable<IEnumerable<T>> sequences, bool isWithRepetitions = false)
		{
			IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
			return sequences.Aggregate(
				emptyProduct,
				(accumulator, sequence) =>
					from accumulatorSequence in accumulator
					from item in sequence
					where !accumulatorSequence.Contains(item) || isWithRepetitions
					select accumulatorSequence.Concat(new[] { item }));
		}
	}
}
